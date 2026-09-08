import { useState } from "react"

function HabitoCard({nome, descricao, id}){
    const [concluido, setConcluido] = useState(false)

    function marcarConcluido(novoEstado){
    
    const token = localStorage.getItem("token")
    
      setConcluido(novoEstado)

      fetch("http://localhost:5259/api/HabitosLog",{

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

    }
    
    return(
        <div className="habito-card">
            <div>
            <h3>{nome}</h3>
            <p>{descricao}</p>
            </div>

            <input
                type="checkbox"
                checked={concluido}
                onChange={() => marcarConcluido(!concluido)}
            />
        </div>
    )
}




export default HabitoCard