import React, {useCallback, useEffect} from "react";
import { userManager } from "../util/oidc";
import { useHistory } from 'react-router-dom';
import ErrorImg from "../images/msg_error_0.png"

export function NotFound() {
    const history = useHistory();
    const handleOk = useCallback(() => {
        history.push("/dashboard");
    }, []);
    return <div style={{display: "flex", justifyContent:"center", alignItems: "center", width:"100%", height: "100vh"}}>
        <div className="window" style={{width: "300px"}}>
        <div className="title-bar">
            <div className="title-bar-text">Page not found</div>
            <div className="title-bar-controls">
                <button aria-label="Minimize"/>
                <button aria-label="Maximize"/>
                <button aria-label="Close"/>
            </div>
        </div>
        <div className="window-body">
            <section className="field-row" >
            <img src={ErrorImg} alt="Error"/>
            <p>Requested page not found</p>
            </section>
            <section className="field-row" style={{justifyContent: "flex-end"}}>
                <button onClick={handleOk}>Go to dashboard</button>
            </section>
        </div>
    </div>
    </div>
}