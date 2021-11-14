import React, { useEffect } from "react";
import { userManager } from "../util/oidc";
import { useHistory } from 'react-router-dom';

export function SignIn() {
    const history = useHistory()
    useEffect(() => {
        async function signinAsync() {
            await userManager.signinRedirectCallback();
            history.push('/dashboard')
        }
        signinAsync()
    }, [history])

    return <p>
        ...
    </p>
}