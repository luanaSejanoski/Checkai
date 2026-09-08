import { useState } from "react"

function HabitoCard({id, nome, descricao, concluido}){
    const [concluidoHoje, setConcluidoHoje] = useState(concluido)

    async function marcarConcluido(novoEstado){
    
    const token = localStorage.getItem("token")
    
      setConcluidoHoje(novoEstado)

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

    }
    
    return(
        <div className="habito-card">
            <div>
            <h3>{nome}</h3>
            <p>{descricao}</p>
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