import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const IndexInspectionTrainn = Loadable(lazy(() => import('../views/InspectionTrain')));
const CreateInspectionTrainn = Loadable(lazy(() => import('../views/InspectionTrain/Create')));
const InspectionTrainnRoute = {
    path: '/InspectionTrain',
    element: <MainLayout/>,
    children: [
        {
            path: '/InspectionTrain',
            element: <IndexInspectionTrainn/>
        },
        {
            path: '/InspectionTrain/create',
            element: <CreateInspectionTrainn/>
        }
    ]
 };

export default InspectionTrainnRoute;
