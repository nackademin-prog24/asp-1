import React, {createContext, useContext, useState } from 'react'

const AuthContext = createContext()

export const AuthProvider = ({children}) => {
    const apiUri = 'https://localhost:7277/api/auth'
    const [token, setToken] = useState(null)
    const [isAdmin, setIsAdmin] = useState(false)
    const [adminApiKey, setAdminApiKey] = useState(null)

    const signUp = async (formData) => {
        const res = await fetch(`${apiUri}/signup`, {
            method: 'POST',
            headers: {
                'Content-Type': 'multipart/form-data'
            },
            body: formData
        })

        if (res.ok) {
            return true
        }

        return false
    }

    const signIn = async (email, password) => {
        const res = await fetch(`${apiUri}/signin`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        })

        if (res.ok) {
            const data = await res.json()
            setToken(data.accessToken)
            setIsAdmin(data.isAdmin)

            if (data.apiKey !== null)
                setAdminApiKey(data.apiKey)

            return true
        }

        return false
    }

    const signOut = async () => {
        const res = await fetch(`${apiUri}/signout`)
        
        if (res.ok) {
            setToken(null)
            setIsAdmin(false)
            setAdminApiKey(null)
        }
    }

    return (
        <AuthContext.Provider value={{token, isAdmin, adminApiKey, signUp, signIn, signOut}}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => useContext(AuthContext)