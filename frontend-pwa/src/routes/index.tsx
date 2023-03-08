import { useRoutes } from 'react-router-dom';
import MainRoutes from './MainRoutes';
import AuthenticationRoutes from './AuthenticationRoutes';
import { lazy } from 'react';
import Loadable from '../components/LoadComponent/Loadable';
import MinimalLayout from '../layout/MinimalLayout';
import AuthRoutes from './AuthRoutes';
import UserRoutes from './UserRoutes';
import AccessProgramRoute from './AccessProgram';
import CategoryRoute from './Category';
import DocumentRoute from './Document';
import InspectionTrainRoute from './InspectionTrain';
import InspectionTrainReportRoute from './InspectionTrainReport'
const Login = Loadable(lazy(() => import('../components/Security/Login')));

const rutaServidor = "";//PRUBEA
//const rutaServidor = "/FO"; //Produccion
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
    CategoryRoute,
    DocumentRoute,
    AuthenticationRoutes,
    AuthRoutes,
    UserRoutes,
    InspectionTrainRoute,
    InspectionTrainReportRoute]);
}
