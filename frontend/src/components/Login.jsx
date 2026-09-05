import { useState } from "react"

function Login({onLogin}){
    
    const[email, setEmail] = useState('');
    const[senha, setSenha] = useState('');
    
    return(
        <div>
            <h1>Login</h1>
 
            <div className= "campo">
            <input
             type="email"
             name="email" 
             onChange={(event) => setEmail(event.target.value)}/><br />

            <input
             type="password"
             name="senha"
             onChange={(event) => setSenha(event.target.value)}
              />
            </div>

            <button onClick={() => onLogin(true)}>Entrar</button>
        </div>
    )
    
}


export default Login