window.loginWithFacebook = function (dotNetHelper) {
    if (typeof FB === "undefined") {
        alert("El SDK de Facebook no está cargado.");
        return;
    }
    FB.login(function (response) {
        if (response.authResponse) {
            dotNetHelper.invokeMethodAsync('OnFacebookLogin', response.authResponse.accessToken);
        } else {
            dotNetHelper.invokeMethodAsync('OnFacebookLogin', null);
        }
    }, { scope: 'pages_manage_posts,pages_read_engagement,pages_show_list' });
};