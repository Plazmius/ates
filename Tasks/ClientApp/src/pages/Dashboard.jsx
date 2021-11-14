import React from "react";
import { useQuery } from 'react-query'
import { request } from "../util/fetch";

export function Dashboard() {
    const { isLoading, error, data } = useQuery('tasks', () =>
        request('/api/tasks', { method: "GET" }).then(async res => {
            return res.status === 200 ? await res.json() : [];
        }
        )
    )
    if (data && data.length === 0) {
        return <p>No tasks for now</p>
    }
    if (isLoading) {
        return <p>...</p>
    }
    return <div style={{
        display: "grid",
        gridTemplateColumns: "1fr 1fr 1fr"
    }}>
        {data.map(x => <p>{x.description}</p>)}
    </div>
}