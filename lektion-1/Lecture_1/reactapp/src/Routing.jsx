import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Clients from './pages/Clients'
import Home from './pages/Home'

const Routing = () => {
  return (
    <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/admin/clients" element={<Clients />} />
    </Routes>
  )
}

export default Routing