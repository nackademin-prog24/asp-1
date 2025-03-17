import React, { useContext } from 'react'
import { ClientsContext } from '../../../contexts/ClientsContext'

const Clients = () => {
  const { clients, loading } = useContext(ClientsContext)

  return (
    <div>

       {
        clients.map(client => (
          <div key={client.id}>{client.clientName}</div>
        ))
       }

    </div>
  )
}

export default Clients