import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { URL_API_V1 } from '..'
import { RootState } from '../..'
import type { LoginRequest, LoginResponse, IRegisterRequest, IRegisterResponse, ITokenApiModel, IUpdatePasswordRequest,IUserResponse,  IUpdateRoleAdminRequest } from '../../types/Auth'


export const authApi = createApi({
  reducerPath: 'UserApi',
  tagTypes: ['User'],
  baseQuery: fetchBaseQuery({
    baseUrl: URL_API_V1,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).auth.token
      if (token) {
        headers.set('authorization', `Bearer ${token}`)
      }
      return headers
    },
  }),
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: 'Account/Login',
        method: 'POST',
        body: credentials,
      }),
    }),
    register: builder.mutation<IRegisterResponse,IRegisterRequest>({
        query:(user) => ({
            url:'Account/Register',
            method:'POST',
            body:user
        })
    }),
    refresh: builder.mutation<LoginResponse,ITokenApiModel>({
      query:(tokenapi) => ({
          url:'Account/RefreshToken',
          method:'POST',
          body:tokenapi
      })
    }),
    updatePassword: builder.mutation<IRegisterResponse,IUpdatePasswordRequest>({
      query:(updatePassword)=>({
        url:'Account/UpdatePassword',
        method:'POST',
        body:updatePassword
      })
    }),
    updateRegister: builder.mutation<IRegisterResponse,IUserResponse>({
      query:(updateRegister)=>({
        url:'Account/UpdateRegister',
        method:'PUT',
        body:updateRegister
      }),
      invalidatesTags:  [{ type: 'User', id:"List" }],
    }),
    getUserByID: builder.query<IUserResponse,string>({
      query:(id)=>`Account/GetUserByID/${id}`,
      providesTags:[{type:"User", id:"List"}]
    }),
    getListGetUsers: builder.query<IUserResponse[], void>({
      query: () => `Account/GetUsers`,
      providesTags:[{type:"User", id:"List"}],      
    }),
    updateRoleAdmin: builder.mutation<IUserResponse,IUpdateRoleAdminRequest>({
      query:(request)=>({
        url:`Account/role/updateRoleAdmin`,
        method:'PUT',
        body:request,
      }),
      invalidatesTags:  [{ type: 'User', id:"List" }],
    }),    
  }),
})

export const { 
  useLoginMutation, 
  useRegisterMutation, 
  useRefreshMutation, 
  useUpdatePasswordMutation,
  useGetUserByIDQuery,
  useUpdateRegisterMutation,
  useGetListGetUsersQuery,
  useUpdateRoleAdminMutation,
 } = authApi
