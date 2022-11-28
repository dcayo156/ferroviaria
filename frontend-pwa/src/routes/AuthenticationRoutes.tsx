import { lazy } from 'react';
import Loadable from '../ui-component/Loadable';
import MinimalLayout from '../layout/MinimalLayout';

const Login = Loadable(lazy(() => import('../components/Security/Login')));
const Register = Loadable(lazy(() => import('../components/Security/Register')));


const AuthenticationRoutes = {
    path: '/authentication',
    element: <MinimalLayout />,
    children: [
        {
            path: '/authentication',
            element: <Login />
        },
        {
            path: '/authentication/login',
            element: <Login />
        },
        {
            path: '/authentication/register',
            element: <Register />
        }
    ]
};

export default AuthenticationRoutes;
