import './assets/css/App.css'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, useRoutes } from "react-router-dom"
import SignIn from './pages/SignIn';
import { AuthProvider } from './contexts/AuthContext';
import Projects from './pages/Projects';

const root = createRoot(document.getElementById('root'));
root.render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <App />
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>
)

function App() {


  return (
    <>
      <SignIn />
      <Projects />
    </>
  )
}

export default App
