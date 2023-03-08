import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';
import styled from '@emotion/styled';
import { useGetInspectionTrainsForYearQuery } from '../../../store/services/InspectionTrain';
ChartJS.register(ArcElement, Tooltip, Legend);
const date= '02-12-2023';
// const toDataURL = (url:any) => fetch(url)
//   .then(response => response.blob())
//   .then(blob => new Promise((resolve, reject) => {
//     const reader = new FileReader()
//     reader.onloadend = () => resolve(reader.result)
//     reader.onerror = reject
//     reader.readAsDataURL(blob)
//   }))
//const { data: InspectionTrainsData, error, isLoading } = useGetFindInspectionTrainsFileByDateQuery(date);
const data = {
    labels: ['Inspeccion Tecnica', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
    datasets: [
      {
        label: '# of Votes',
        data: [12, 19, 3, 5, 2, 3],
        backgroundColor: [
          'rgba(255, 99, 132, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(255, 206, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(255, 159, 64, 0.2)',
        ],
        borderColor: [
          'rgba(255, 99, 132, 1)',
          'rgba(54, 162, 235, 1)',
          'rgba(255, 206, 86, 1)',
          'rgba(75, 192, 192, 1)',
          'rgba(153, 102, 255, 1)',
          'rgba(255, 159, 64, 1)',
        ],
        borderWidth: 1,
      },
    ],
  };
const ChartWrapper = styled.div`
  max-width: 700px;
  margin: 0 auto;
`;

// interface FormAccessProgramProps {
//   accessprogram: IInspectionTrainPieChartFullVm
//   setAccessProgram: (value: React.SetStateAction<IInspectionTrainPieChartFullVm>) => void
// }
  const PieChart: React.FunctionComponent = () => {
      const { data: dataPie, error, isLoading:load } = useGetInspectionTrainsForYearQuery();
    console.log("DataPie",dataPie)
 return  <Pie  
  data={dataPie?dataPie:data} 
  style={{ width:"500px", margin: "0 auto"}}
 />
};
export default PieChart;