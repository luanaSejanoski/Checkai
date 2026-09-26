import { useState } from "react"

function Login({onLogin}){
    
    const[email, setEmail] = useState('');
    const[senha, setSenha] = useState('');
    const[nome, setNome] = useState('');
    
    return(
        <div>
            <h1>Login</h1>
 
            <div className= "campo">
            <input
             type="text"
             name="nome" 
             placeholder="Nome"
             onChange={(event) => setNome(event.target.value)}/><br /><br />

            <input
             type="email"
             name="email" 
             placeholder="E-mail"
             onChange={(event) => setEmail(event.target.value)}/><br /><br />

            <input
             type="password"
             name="senha"
             placeholder="Senha"
             onChange={(event) => setSenha(event.target.value)}
            />
            </div>

            <button onClick={async() =>{

             const resposta = await fetch("http://localhost:5259/api/Usuario/login", {
             
                method: "POST",

                headers:{
                    "Content-Type": "application/json"
                },

                body: JSON.stringify({
                    nome,
                    email,
                    senha
                })
            })

            const dados = await resposta.json();

            console.log(dados);

            if(resposta.ok){
                localStorage.setItem("token", dados.token)
                onLogin(true)
            }
        }}>
            Entrar
            </button>
        
        </div>
    )
    
}


export default Login