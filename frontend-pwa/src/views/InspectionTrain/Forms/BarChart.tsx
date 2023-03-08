import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';

import { Bar } from 'react-chartjs-2';
import { faker } from '@faker-js/faker';
import { useGetInspectionTrainsForYearQuery } from '../../../store/services/InspectionTrain';
import { sizeWidth } from '@mui/system';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);



// const BarChart: React.FunctionComponent = () => {
//     const options = {
//         responsive: true,
//         plugins: {
//           legend: {
//             position: 'top' as const,
//           },
//           title: {
//             display: true,
//             text: 'Chart.js Bar Chart',
//           },
//         },
//       };
      
    const labels = ['2023', '204'];
    const data = {
        labels,
        datasets: [
          {
            label: 'Aspectos básicos',
            data: [50, 12],
            backgroundColor: 'rgba(255, 99, 132, 0.5)',
          },
          {
            label: 'Aspectos técnicos',
            data:[20, 19],
            backgroundColor: 'rgba(53, 162, 235, 0.5)',
          },          
          {
            label: 'Dataset 3',
            data:[10, 19],
            backgroundColor: 'rgba(53, 100, 235, 0.5)',
          }
        ],
      };
    
// return  <Bar options={options} data={data} />;
//}
//export default BarChart;

const BarChart: React.FunctionComponent = () => {
  const { data: dataBar, error, isLoading:load } = useGetInspectionTrainsForYearQuery();
  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: 'Inspección integral de trenes Anual',
      },
    },
  };
//console.log("dataBar",dataBar)
return  <Bar options={options} 
  data={dataBar?dataBar:data} 
 // style={{ width:"2000px", margin: "0 auto"}}
/>;
// return  <Pie  
// data={dataBar?dataBar:data} 
// style={{ width:"500px", margin: "0 auto"}}
// />
};
export default BarChart;

