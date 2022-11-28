export interface LoginRequest {
    email: string
    password: string
}
export interface LoginResponse{
    id: string | null
    username: string | null
    email: string | null
    token: string | null
    refreshToken:string | null
}

export interface IRegisterRequest {
    nombre: string
    apellidos: string
    email: string
    username: string
    password: string
    confirmpassword:string
}
export interface IRegisterResponse {
    userId: string
    username : string
    email: string   
    token: string
}
export interface ITokenApiModel{
    accessToken: string
    refreshToken: string
}

export interface IUpdatePasswordRequest{
    id: string
    currentPassword: string
    newPassword: string
}
export interface IUserResponse {
    id: string
    nombre: string
    apellidos: string
    email: string
    username: string
    admin?:boolean
}
export interface IUserRequest {
    id :string
    nombre: string
    apellidos: string
    email: string
    username: string
    password: string
    confirmpassword:string
}
export interface IUpdateRoleAdminRequest {
    id :string
    Status:boolean
}