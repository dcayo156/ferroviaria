import { LoginResponse } from "../../types/Auth";
import jwt_decode from "jwt-decode";

export interface Remember{
    email:string
    password:string
    remember:boolean
}
export const remember=(user:Remember)=>{
    let {email,password,remember}=user;
    window.localStorage.setItem("LJN_email",email);
    window.localStorage.setItem("LJN_password",password);
    window.localStorage.setItem("LJN_remember",remember?"true":"false");
}
export const getRemember=():Remember=>{
    let data:Remember={
        email:"",
        password:"",
        remember:false
    }
    data.email = window.localStorage.getItem("LJN_email")==null?"":window.localStorage.getItem("LJN_email") as string;
    data.password = window.localStorage.getItem("LJN_password")==null?"":window.localStorage.getItem("LJN_password") as string;
    let r:string=window.localStorage.getItem("LJN_remember")==null?"false":window.localStorage.getItem("LJN_remember") as string;
    data.remember= r==="true";
    return data;
}
export const forget=()=>{
    window.localStorage.removeItem("LJN_email");
    window.localStorage.removeItem("LJN_password");
    window.localStorage.removeItem("LJN_remember");
}
export const authenticate=(data:LoginResponse)=>{
    let dat:string=JSON.stringify(data);
    let token=data.token?data.token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIiLCJlbWFpbCI6IiIsInVpZCI6IiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IiIsImV4cCI6MCwiaXNzIjoiIiwiYXVkIjoiIn0.0r39Y6PLoC_FuUaX4PVvxw12qms0uOe7PX-lTR3JKyw";
    window.localStorage.setItem("LJN_credentials",dat);
    window.localStorage.setItem("LJN_token",token);
    window.localStorage.setItem("LJN_refreshtoken",data.refreshToken!);
    var decoded:any = jwt_decode(token);
    window.localStorage.setItem("LJN_token_exp",decoded.role);
}
export const logout=()=>{
    window.localStorage.removeItem("LJN_credentials");
    window.localStorage.removeItem("LJN_token");
    window.localStorage.removeItem("LJN_refreshtoken");
    forget();
}
export const getCredentials=():LoginResponse=>{
    let data:LoginResponse={
        id:null,
        email:null,
        username:null,
        token:null,
        refreshToken:null,
    }
    let credentials:string|null=window.localStorage.getItem("LJN_credentials");
    if(credentials!=null){
        data=JSON.parse(credentials as string);
    }
    return data;
}
export const getToken=()=>{
    return window.localStorage.getItem("LJN_token");
}
export const getTokenExp=()=>{
    return parseInt(window.localStorage.getItem("LJN_token_exp")!);
}