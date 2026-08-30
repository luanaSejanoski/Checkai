function CardResumo({titulo, valor, icone}){
    return(
        <div className="card"> 
            <span>{icone}</span>
            <h3>{titulo}</h3>
            <strong>{valor}</strong>
        </div>
    )
}
export default CardResumo