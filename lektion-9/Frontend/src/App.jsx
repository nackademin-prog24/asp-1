import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [formData, setFormData] = useState({ email: "john.doe@domain.com", password: "BytMig123!", rememberMe: false})
  const [users, setUsers] = useState([])

  const signIn = async () => {    
    const res = await fetch('https://localhost:7032/api/auth/signin', {
      method : 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(formData)
    })

    if (res.status === 200) {
      const data = await res.json()
      localStorage.setItem('accesstoken', data.token)
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    await signIn()
    await getUsers()
  }

  const getUsers = async () => {
    const res = await fetch('https://localhost:7032/api/users', {
      method: 'GET',
      headers: {
        'Authorization': `bearer ${localStorage.getItem('accesstoken')}`
      }
    })

    if (res.ok) {
      const data = await res.json()
      setUsers(data)
    }
  }

  return (
    <>
      <form onSubmit={handleSubmit}>
        <button type="submit">SUBMIT</button>
      </form>

      <div>
          {
            users.map(user => (
              <div key={user.id}>{user.displayName}</div>
            ))
          }
      </div>
    </>
  )
}

export default App
