import React, { useState } from 'react'
import { useAuth } from '../contexts/AuthContext'

const SignIn = () => {
    const { signIn } = useAuth()
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const handleSubmit = async (e) => {
        e.preventDefault()

        const succeeded = await signIn(email, password)
        if (succeeded)
            console.log('inloggning lyckades')
        else
            console.log('inloggning misslyckades')
    }

    return (
        <div id="signin" className="card">
            <div className="card-header">
                <h1>Login</h1>
            </div>
            <div className="card-body">

                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Email:</label>
                        <input
                            type="email"
                            placeholder="ange din e-post"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>

                    <div>
                        <label>Password</label>
                        <input
                            type="password"
                            placeholder="ange ditt lösenord"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                        
                    <button type="submit">Logga in</button>
                </form>

            </div>
        </div>
    )
}

export default SignIn