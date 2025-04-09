import { Navigate } from "react-router-dom"
import { useAuth } from "./contexts/AuthContext"

export const ProtectedRoute = ({children}) => {
    const { token } = useAuth()
    return token ? children : <Navigate to="/signin" />    
}

export const AdminRoute = ({children}) => {
    const { token, isAdmin } = useAuth()

    if (!token) return <Navigate to="/signin" /> 
    if (!isAdmin) return <Navigate to="/accessdenied" /> 

    return children    
}