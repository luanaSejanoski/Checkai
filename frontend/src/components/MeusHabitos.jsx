import { useState, useEffect } from "react"
import HabitoCard from './HabitoCard'

function MeusHabitos() {
    const [habitos, setHabitos] = useState([]);
    const [mostrarForm, setMostrarForm] = useState(false);
    const [nome, setNome] = useState("");
    const [descricao, setDescricao] = useState("");
    const [metaDias, setMetaDias] = useState("");

    async function buscarHabitos() {
        
     const token = localStorage.getItem("token");

     const resposta = await fetch("http://localhost:5259/api/Habitos",{

        method: "GET",

        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
     })

        const dados = await resposta.json();
        setHabitos(dados);
    }
    useEffect(() => {
      buscarHabitos()
}, [])

    async function CriarHabito() {
     const token = localStorage.getItem("token");
     const resposta = await fetch("http://localhost:5259/api/Habitos", {

        method: "POST",

        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

        body: JSON.stringify({
            nome: nome,
            descricao: descricao,
            metaDias: metaDias
        })
     });

     const dados = await resposta.json();

     if(resposta.ok){
        console.log(dados);
     }
}

    async function ExcluirHabito(id){
    const token = localStorage.getItem("token");
    const resposta = await fetch(`http://localhost:5259/api/Habitos/${id}`,{

        method: "DELETE",

        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    });

    if(resposta.ok){
        buscarHabitos();
    }
}

  return (
    <>
        <div className="cabecalho-habitos">
            <h1 className="meus-habitos-titulo">Meus hábitos</h1>

            <div className="acoes-habitos-form">
            <button className="adicionar-habito" onClick={
                () => setMostrarForm(true)
            }>
                + Adicionar hábito</button> 

        {mostrarForm && (
            <div className="form-habitos">

                <div className="campo-formulario">
                <label htmlFor="nome">Nome:</label>
                <input
                    id="nome"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                /> <br /><br />
                </div>

                <div className="campo-formulario">
                <label htmlFor="descricao">Descrição:</label>
                <input 
                    id="descricao"
                    value={descricao}
                    onChange={(e) => setDescricao(e.target.value)}
                    /> <br /><br />
                </div>

                <div className="campo-formulario">
                <label htmlFor="metaDias">Meta de dias: </label>
                <input 
                    id="metaDias"
                    value={metaDias}
                    onChange={(e) => setMetaDias(e.target.value)} 
                    /> <br /><br />
                </div>
         
                <div className="botoes">
                <button onClick={() => CriarHabito()}
                    >Salvar</button>
                <button onClick={() => setMostrarForm(false)}
                    >Cancelar</button>
                </div>
                
            </div>
)}
          </div>
        </div>

        <div className="habitos-grid">
            {habitos.map((habito) => (
                <HabitoCard
                    key={habito.id}
                    id={habito.id}
                    nome={habito.nome}
                    descricao={habito.descricao}
                    metaDias={habito.metaDias}
                    excluir={ExcluirHabito}
                    buscarHabitos={buscarHabitos}
                    modo="gerenciamento"
                />
            ))}
        </div>
    </>
)
}

export default MeusHabitos;