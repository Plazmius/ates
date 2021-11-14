import React, { useEffect } from "react";
import { useLocation, Redirect } from "react-router-dom"
function useQuery() {
    const { search } = useLocation();

    return React.useMemo(() => new URLSearchParams(search), [search]);
}

export function Callback() {
    return <Redirect to="/dashboard" />
}