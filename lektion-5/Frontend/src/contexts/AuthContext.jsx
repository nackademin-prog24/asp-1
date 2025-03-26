import { useContext, createContext, useState, useEffect } from "react";

const AuthContext = createContext()

export const AuthProvider = ({children}) => {
    const apiUrl = "https://localhost:7273/api/signin"
    const [loading, setLoading] = useState(true)
    const [token, setToken] = useState(null)
    const [user, setUser] = useState(null)
    const [errorMessage, setErrorMessage] = useState(null)

    useEffect(() => {
        const storeToken = localStorage.getItem('authToken')
        if (storeToken)
            setToken(storeToken)

        setLoading(false)
    },[])

    const signUp = async () => {

    }

    const signIn = async (email, password, isPersistent = false) => {
        try {

            const res = await fetch(apiUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({email, password, isPersistent})
            })

            const data = await res.json()

            if (!res.ok) {             
                setErrorMessage(data.error)
                return false
            }

            setToken(data.token)
            localStorage.setItem('authToken', data.token)
            return true
        }
        catch {
            setErrorMessage("Invalid email or password")
            return false
        }
    }

    const signOut = () => {
        setToken(null)
        setUser(null)
        localStorage.removeItem('authToken')
    }

    const authFetch = async (url, options = {}) => {
        const headers = options.headers ? { ...options.headers } : {}
        if (token){
            headers['Authorization'] = `Bearer ${token}`
        }
        return fetch(url, {...options, headers})
    }


    return (
        <AuthContext.Provider value={{loading, token, user, signUp, signIn, signOut, authFetch}}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => useContext(AuthContext)