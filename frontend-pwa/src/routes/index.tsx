import { useRoutes } from 'react-router-dom';
import MainRoutes from './MainRoutes';
import AuthenticationRoutes from './AuthenticationRoutes';
import { lazy } from 'react';
import Loadable from '../components/LoadComponent/Loadable';
import MinimalLayout from '../layout/MinimalLayout';
import ReltaionRoutes from './ReltionshipRoutes';
import AuthRoutes from './AuthRoutes';
import UserRoutes from './UserRoutes';

const Login = Loadable(lazy(() => import('../components/Security/Login')));

export default function ThemeRoutes() {
    return useRoutes([{
        path: '/',
        element: <MinimalLayout />,
        children: [
            {
                path: '/',
                element: <Login />
            }
        ]
    },
    MainRoutes,
    AuthenticationRoutes,
    AuthRoutes,
    UserRoutes]);
}
