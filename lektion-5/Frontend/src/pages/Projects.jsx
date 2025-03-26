import React, { useEffect, useState } from 'react'
import { useAuth } from '../contexts/AuthContext'

const Projects = () => {
    const {token, authFetch} = useAuth()
    const [projects, setProjects] = useState([])

    const getProjects = async () => {
        // const res = await fetch('https://localhost:7273/api/projects', {
        //     headers: {
        //         'Authorization': `bearer ${token}`,
        //         'Content-Type': 'application/json'
        //     }
        // })

        const res = await authFetch('https://localhost:7273/api/projects')

        if (res.ok) {
            const data = await res.json()
            setProjects(data)
        } 
    }

    useEffect(() => {
        getProjects()
    }, [token])

    return (
        <div>
            <h1>Projects</h1>
            {
                projects.map(project => (
                    <div>{project}</div>
                ))
            }
        </div>
    )
}

export default Projects