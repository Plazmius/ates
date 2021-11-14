import Oidc from "oidc-client";
const settings = {
    authority: "https://localhost:5001",
    client_id: "tasks",
    response_type: "code",
    scope: "openid profile tasks",

    redirect_uri: `${window.location.host}/signin-oidc`,
    post_logout_redirect_uri: `${window.location.host}/signout-oidc`,

    automaticSilentRenew: false,
    silent_redirect_uri: `${window.location.host}/silent-signin-oidc`
};
export const userManager = new Oidc.UserManager(settings);