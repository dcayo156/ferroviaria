import {
  BaseQueryFn,
  FetchArgs,
  fetchBaseQuery,
  FetchBaseQueryError,
} from "@reduxjs/toolkit/query/react";
import { toast } from 'react-toastify';
import { URL_API_V1 } from ".";
import { authenticate, getCredentials, logout } from "../slices/Auth/localStorage";
import { LoginResponse } from "../types/Auth";

const baseQuery = fetchBaseQuery({
  baseUrl: URL_API_V1,
  prepareHeaders: (headers, { getState }) => {
    const token = (getCredentials() as LoginResponse).token;
    if (token) {
      headers.set("authorization", `Bearer ${token}`);
    }
    return headers;
  },
});

export const baseQueryWithReauth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  if(result.error && result.error.status === 500){
    toast.error("La solicitud no ha podido ser finalizada");
  }
  if(result.error && result.error.status === 404){
    toast.error("Objetivo no encontrado");
  }
  if (result.error && result.error.status === 401) {
    let token = (getCredentials() as LoginResponse).token;
    let refreshToken = (getCredentials() as LoginResponse).refreshToken;

    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ accessToken: token, refreshToken: refreshToken }),
    };

    let refreshTokenRequest= await fetch(`${URL_API_V1}Account/RefreshToken`, requestOptions);
    if(!refreshTokenRequest.ok)
    {
      logout();
      window.location.href = "/";
    }
    let responseRefreshToken = await refreshTokenRequest.json();
    if(responseRefreshToken)
    authenticate(responseRefreshToken as LoginResponse);
    result = await baseQuery(args, api, extraOptions);
    
  }
  return result;
};
