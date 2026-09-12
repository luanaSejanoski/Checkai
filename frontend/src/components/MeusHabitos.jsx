import { useState, useEffect } from "react"
import HabitoCard from './HabitoCard'

function MeusHabitos() {
    const [habitos, setHabitos] = useState([]);

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

  return (
    <>
        <h1>Meus hábitos</h1>

        <div className="habitos-grid">
            {habitos.map((habito) => (
                <HabitoCard
                    key={habito.id}
                    id={habito.id}
                    nome={habito.nome}
                    descricao={habito.descricao}
                    metaDias={habito.metaDias}
                />
            ))}
        </div>
    </>
)
}

export default MeusHabitos;