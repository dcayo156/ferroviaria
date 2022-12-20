import { useRoutes } from 'react-router-dom';
import MainRoutes from './MainRoutes';
import AuthenticationRoutes from './AuthenticationRoutes';
import { lazy } from 'react';
import Loadable from '../components/LoadComponent/Loadable';
import MinimalLayout from '../layout/MinimalLayout';
import ReltaionRoutes from './delete-ReltionshipRoutes';
import AuthRoutes from './AuthRoutes';
import UserRoutes from './UserRoutes';
import AccessProgramRoute from './AccessProgram';

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
    AccessProgramRoute,
    AuthenticationRoutes,
    AuthRoutes,
    UserRoutes]);
}
