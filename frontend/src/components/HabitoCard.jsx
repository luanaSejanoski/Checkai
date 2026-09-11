import { useState, useEffect } from "react"

function HabitoCard({id, nome, descricao, concluido, atualizarLogs, buscarMaiorSequencia}){
  
    const [concluidoHoje, setConcluidoHoje] = useState(concluido)
    const [sequenciaAtual, setSequenciaAtual] = useState(0);

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
    
    return(
        <div className="habito-card">
            <div>
            <h3>{nome}</h3>
            <p>{descricao}</p>
            <p>Sequência atual: 🔥{sequenciaAtual}</p>
            </div>

            <input
                type="checkbox"
                checked={concluidoHoje}
                onChange={() => marcarConcluido(!concluidoHoje)}
            />
        </div>
    )
}

export default HabitoCard