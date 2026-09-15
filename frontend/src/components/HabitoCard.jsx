import { useState, useEffect } from "react"

function HabitoCard({id, nome, descricao, concluido, atualizarLogs, buscarMaiorSequencia, metaDias, modo, excluir, buscarHabitos}){
  
    const [concluidoHoje, setConcluidoHoje] = useState(concluido)
    const [sequenciaAtual, setSequenciaAtual] = useState(0)
    const [editando, setEditando] = useState(false)
    const [nomeEditado, setNomeEditado] = useState(nome)
    const [descEditada, setDescEditada ] = useState(descricao)
    const [metaDiasEditado, setMetaDiasEditado] = useState(metaDias)

      useEffect(() => {
      buscarSequencia()
}, [id])

    async function buscarSequencia(){

        const token = localStorage.getItem("token")

        const resposta = await fetch(`http://localhost:5259/api/HabitosLog/sequencia/${id}`,{

        method: "GET",

        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        })

        const dados = await resposta.json();
        setSequenciaAtual(dados);
        
        console.log(dados)

    }

    useEffect(() => {
    setConcluidoHoje(concluido)
}, [concluido])

    async function marcarConcluido(novoEstado){
    
    const token = localStorage.getItem("token")
    
      setConcluidoHoje(novoEstado)

      atualizarLogs(id, novoEstado)

      const resposta = await fetch("http://localhost:5259/api/HabitosLog",{

        method: "POST",

        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

        body: JSON.stringify({
            habitoId: id,
            concluido: novoEstado
        })
      })
      const dados = await resposta.json()
      console.log(dados)

    if(resposta.ok){
        buscarMaiorSequencia();
        buscarSequencia();
    }
    }

    async function salvarEdicao(id) {

        console.log("Meta dias enviada:", metaDiasEditado)

      const token = localStorage.getItem("token");

      const resposta = await fetch(`http://localhost:5259/api/Habitos/${id}`,{
        
      method: "PUT",

      headers:{
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      },

      body: JSON.stringify({
        nome: nomeEditado,
        descricao: descEditada,
        metaDias: metaDiasEditado
      })  
    });

    console.log("status:", resposta.status);

      if(resposta.ok){
        buscarHabitos();
        setEditando(false);
      }
}

    return(
        <div className={modo === "gerenciamento" ? "habito-card gerenciamento" : "habito-card"}>
            <div>
            <h3>{nome}</h3>
            <p>{descricao}</p>
            {modo === "dashboard" &&(
                <p>Sequência atual: 🔥{sequenciaAtual}</p>
            )}
            <p>Meta: {metaDias} dias </p>

            {modo === "gerenciamento" && (
    <>
        {editando && (
            <div className="form-edicao">
                <input
                    value={nomeEditado}
                    onChange={(e) => setNomeEditado(e.target.value)}
                />

                <input
                    value={descEditada}
                    onChange={(e) => setDescEditada(e.target.value)}
                />

                <input
                    value={metaDiasEditado}
                    onChange={(e) => setMetaDiasEditado(e.target.value)}
                />

                <div className="botoes">
                    <button onClick={() => salvarEdicao(id)}>
                        Salvar
                    </button>

                    <button onClick={() => setEditando(false)}>
                        Cancelar
                    </button>
                </div>
            </div>
        )}

        {!editando && (
            <div className="botoes">
                <button onClick={() => setEditando(true)}>
                    Editar
                </button>

                <button onClick={() => excluir(id)}>
                    Excluir
                </button>
            </div>
        )}
    </>
)}
            </div>
            {modo === "dashboard" &&(
                <input
                    type="checkbox"
                    checked={concluidoHoje}
                    onChange={() => marcarConcluido(!concluidoHoje)}
                />
            )}
        </div>
    )

}
export default HabitoCard