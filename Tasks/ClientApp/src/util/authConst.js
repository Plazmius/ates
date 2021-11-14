const authUrl = "https://localhost:5001";
export const IDENTITY_CONFIG = {
  authority: authUrl,
  client_id: "tasks-spa",
  redirect_uri: `${window.location.origin}/signin-oidc`,
  silent_redirect_uri: `${window.location.origin}/silentrenew`,
  post_logout_redirect_uri: `${window.location.origin}/logout`,
  response_type: "code",
  automaticSilentRenew: false,
  loadUserInfo: false,
  scope: 'tasks profile openid'
};

export const METADATA_OIDC = {
  jwks_uri:
    authUrl + "/.well-known/openid-configuration/jwks",
  authorization_endpoint: authUrl + "/connect/authorize",
  token_endpoint: authUrl + "/connect/token",
  userinfo_endpoint: authUrl + "/connect/userinfo",
  end_session_endpoint: authUrl + "/connect/endsession",
  check_session_iframe:
    authUrl + "/connect/checksession",
  revocation_endpoint: authUrl + "/connect/revocation",
  introspection_endpoint: authUrl + "/connect/introspect"
};
