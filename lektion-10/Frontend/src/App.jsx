import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext'
import { UserProvider } from './contexts/UserContext'
import './App.css'
import SignIn from './pages/SignIn'
import SignUp from './pages/SignUp'
import Users from './pages/Users'
import AccessDenied from './pages/AccessDenied'
import { AdminRoute, ProtectedRoute } from './ProtectedRoute'
import Clients from './pages/Clients'

function App({children}) {
  return (
    <BrowserRouter>
      <AuthProvider>
      <UserProvider>

        <Routes>

          <Route path="/signup" element={<SignUp />} />
          <Route path="/signin" element={<SignIn />} />
          <Route path="/accessdenied" element={<AccessDenied />} />

          <Route path="/users" element={
            <ProtectedRoute>
              <Users />
            </ProtectedRoute>
          } />

          <Route path="/clients" element={
            <AdminRoute>
              <Clients />
            </AdminRoute>
          } />

          <Route path="/" element={
            <ProtectedRoute>
              <Users />
            </ProtectedRoute>
          } />

        </Routes>

      </UserProvider>
      </AuthProvider>
    </BrowserRouter>
  )
}

export default App
