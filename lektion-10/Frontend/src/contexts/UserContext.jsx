import React, {createContext, useContext, useEffect, useState } from 'react'
import { useAuth } from './AuthContext'

const UserContext = createContext()

export const UserProvider = ({children}) => {
    const { token, adminApiKey } = useAuth()

    const apiUri = 'https://localhost:7277/api/users'
    const [users, setUsers] = useState([])

    const getUsers = async () => {
        try {
            const res = await fetch(`${apiUri}`, {
                method: 'GET',
                headers: {
                    'Authorization': `bearer ${token}`
                }
            })
    
            if (res.ok) {
                const data = await res.json()
                setUsers(data)
            }
        } catch (error) {
            console.error(error)
        }
    }

    useEffect(() => {
        getUsers()
    }, [])

    return (
        <UserContext.Provider value={{users, getUsers}}>
            {children}
        </UserContext.Provider>
    )
}

export const useUser = () => useContext(UserContext)