import { useState } from "react"

function HabitoCard({nome, descricao}){
    const [concluido, setConcluido] = useState(false)
    return(
        <div className="habito-card">
            <div>
            <h3>{nome}</h3>
            <p>{descricao}</p>
            </div>

            <input
                type="checkbox"
                checked={concluido}
                onChange={() => setConcluido(!concluido)}
            />
        </div>
    )

}

export default HabitoCard