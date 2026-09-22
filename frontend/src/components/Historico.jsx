import { useState, useEffect } from "react"

function Historico(){

    const [habitos, setHabitos] = useState([]);
    const [habSelecionado, setHabSelecionado] = useState("");
    const [historico, setHistorico] = useState([]);
    const [dropdownAberto, setDropdownAberto] = useState(false);
    const [pesquisa, setPesquisa] = useState("");

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

        const resposta = await fetch("http://localhost:5259/api/Habitos/todos", {

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
        });

        const dados = await resposta.json();

        setHistorico(dados);
        console.log(dados);
    }

    return (
        <div>

    <div className="lista-habitos">

    <input
        type="text"
        placeholder="Pesquisar hábito..."
        value={pesquisa}
        onChange={(e) => setPesquisa(e.target.value)}
    />

    <div className="opcoes-habitos">
    {habitos
        .filter(habito =>
            habito.nome.toLowerCase().includes(pesquisa.toLowerCase())
        )
        .map((habito) => (
            <div
                className="item-habito"
                key={habito.id}
                onClick={() => {
                    setHabSelecionado(habito.id);
                }}
            >
                {habito.nome}
            </div>
        ))}
        </div>
</div>
        

         <div className="historico-grid">
        {historico.map((dia) =>(
            <div className={dia.concluido ? "dia-historico concluido" : "dia-historico nao-concluido"} 
                key={dia.data}>
              <p>{new Date(dia.data).toLocaleDateString("pt-BR")}</p>
              <p>{dia.concluido ? "✓" : "✗"}</p>
              {dia.mensagem &&(
               <p>{dia.mensagem}</p> 
              )}
            </div>
        ))}
        </div>
        </div>
    )
}

export default Historico
