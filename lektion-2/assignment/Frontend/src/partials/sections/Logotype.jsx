import React from 'react'
import { Link } from 'react-router-dom'

const Logotype = () => {
  return (
    <Link to="/" className="logotype">
        <img src="/images/alpha-logotype.svg" alt="Alpha Logotype" />
        <span>alpha</span>
    </Link>
  )
}

export default Logotype