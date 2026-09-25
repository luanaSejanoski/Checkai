
function Perfil(){
    return(
        <div className="perfil-container">

            <div className="perfil-card">
                <h2>Meu Perfil</h2>

                <label>Nome</label>
                <input type="text" />

                <label>E-mail</label>
                <input type="email" />

                <button>salvar</button><br />
                <button>Cancelar</button>
            </div>

            <div className="jornada-card">

                <h2>Minha jornada</h2>

                <div>
                    <p>Hábitos criados</p>
                    <span>0</span>
                </div>

                <div>
                    <p>Metas concluídas</p>
                    <span>0</span>
                </div>


                <div>
                    <p>Dias registrados</p>
                    <span>0</span>
                </div>
            </div>

        </div>
    );
}

export default Perfil;