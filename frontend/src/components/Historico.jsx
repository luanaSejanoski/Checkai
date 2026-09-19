import { useState, useEffect } from "react"

function Historico(){

    const [habitos, setHabitos] = useState([]);
    const [habSelecionado, setHabSelecionado] = useState("");
    const [historico, setHistorico] = useState([]);

    useEffect(() => {
    buscarHabitos();
}, [])

useEffect(() => {
    if(habSelecionado){
        buscarHistorico(habSelecionado);
    }
}, [habSelecionado])

    async function buscarHabitos() {
        const token = localStorage.getItem("token");

        const resposta = await fetch("http://localhost:5259/api/Habitos", {

         method: "GET",

         headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
         }

        })
        
        const dados = await resposta.json();

        console.log("dados dos hábitos:", dados);
        setHabitos(dados);
        setHabSelecionado(dados[0].id);
    }
    
    async function buscarHistorico(id){
        
        const token = localStorage.getItem("token");

        const resposta = await fetch(`http://localhost:5259/api/HabitosLog/historico/${id}`, {

            method: "GET",

            headers:{
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        })

        const dados = await resposta.json();
        setHistorico(dados);
        console.log(dados);
    }

    return (
        <div>
        <select onChange={(e) => {
        setHabSelecionado(e.target.value)
}}>
        {habitos.map((habito) => (
        <option key={habito.id} value={habito.id}>
            {habito.nome}
        </option>
    ))}
        </select>

        <div className="historico-grid">
        {historico.map((dia) =>(
            <div className={dia.concluido ? "dia-historico concluido" : "dia-historico nao-concluido"} 
                key={dia.data}>
              <p>{new Date(dia.data).toLocaleDateString("pt-BR")}</p>
              <p>{dia.concluido ? "✓" : "✗"}</p>
            </div>
        ))}
        </div>
        </div>
    )
}

export default Historico
