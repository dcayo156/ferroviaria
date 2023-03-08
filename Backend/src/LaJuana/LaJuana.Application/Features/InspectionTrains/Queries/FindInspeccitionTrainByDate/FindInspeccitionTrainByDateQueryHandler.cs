using AutoMapper;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Features.InspectionTrains.Queries.GetListInspectionTrainAll;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Features.InspectionTrains.Queries.FindInspeccitionTrainByDate
{
    public class FindInspeccitionTrainByDateQueryHandler : IRequestHandler<FindInspeccitionTrainByDateQuery, InspectionTrainPieChartFullVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FindInspeccitionTrainByDateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<InspectionTrainPieChartFullVm> Handle(FindInspeccitionTrainByDateQuery request, CancellationToken cancellationToken)
        {
            try
            {            
                var listInspeccionTren = await _unitOfWork.InspectionTrainsRepository.GetListInspectionTrains();

                var yearsList = listInspeccionTren.Select(x => new
                {
                    Year = Convert.ToDateTime(x.CreatedDate).Year,
                }).Distinct().
                    OrderBy(x => x.Year).ToList();


                var inspectionTrainBasicAspects = _mapper.Map<List<InspectionTrainBasicAspects>>(listInspeccionTren);
                var inspectionTrainTechnicalAspects = _mapper.Map<List<InspectionTrainTechnicalAspects>>(listInspeccionTren);
                var inspectionTrainProperHandling = _mapper.Map<List<InspectionTrainProperHandling>>(listInspeccionTren);
    
                var countBasicAspects = new List<int>();
                var countTechnicalAspects = new List<int>();
                var countProperHandling = new List<int>(); 

                foreach (var year in yearsList)
                {
                    List<string> listaBasicAspects = new List<string>();
                    foreach (var item in inspectionTrainBasicAspects.Where(x => Convert.ToDateTime(x.CreatedDate).Year == year.Year))
                    {
                        listaBasicAspects.Add(item?.AspectoBasicoUnoSi);
                        listaBasicAspects.Add(item?.AspectoBasicoDosSi);
                        listaBasicAspects.Add(item?.AspectoBasicoTresSi);
                        listaBasicAspects.Add(item?.AspectoBasicoCuatroSi);
                    }                    

                    List<string> listaTechnicalAspects = new List<string>();
                    foreach (var item in inspectionTrainTechnicalAspects.Where(x => Convert.ToDateTime(x.CreatedDate).Year == year.Year))
                    {
                        listaTechnicalAspects.Add(item?.AspectoTecnicoCincoSi);
                        listaTechnicalAspects.Add(item?.AspectoTecnicoSeisSi);
                        listaTechnicalAspects.Add(item?.AspectoTecnicoSieteSi);
                        listaTechnicalAspects.Add(item?.AspectoTecnicoOchoSi);
                        listaTechnicalAspects.Add(item?.AspectoTecnicoNueveSi);
                        listaTechnicalAspects.Add(item?.AspectoTecnicoDiezSi);
                    }

                    List<string> listaProperHandling = new List<string>();
                    foreach (var item in inspectionTrainProperHandling.Where(x => Convert.ToDateTime(x.CreatedDate).Year == year.Year))
                    {
                        listaProperHandling.Add(item?.MenejoAdecuadoOnceSi);
                        listaProperHandling.Add(item?.MenejoAdecuadoDoceSi);
                        listaProperHandling.Add(item?.MenejoAdecuadoTreceSi);
                    }
                    countBasicAspects.Add(listaBasicAspects.Count(x => x != null && x != string.Empty));
                    countTechnicalAspects.Add(listaTechnicalAspects.Count(x => x != null && x != string.Empty));
                    countProperHandling.Add(listaProperHandling.Count(x => x != null && x != string.Empty));
                }
                var listDataSets = new List<Datasets>();

                //Aspecto Basicos
                var dataBasicAspect = new Datasets()
                {
                    Label = "Aspectos Básicos",
                    data = countBasicAspects.ToArray(),
                    BackgroundColor = "rgba(255, 99, 132, 0.5)"
                };

                //Aspecto Tecnico
                var dataTechnicalAspects = new Datasets()
                {
                    Label = "Aspectos Técnicos",
                    data = countTechnicalAspects.ToArray(),
                    BackgroundColor = "rgba(54, 162, 235, 1)"
                };

                //Manejo adecuado de trenes
                var dataProperHandling = new Datasets()
                {
                    Label = "Manejo adecuado de trenes",
                    data = countProperHandling.ToArray(),
                    BackgroundColor = "rgba(255, 159, 64, 1)"
                };
                listDataSets.Add(dataBasicAspect);
                listDataSets.Add(dataTechnicalAspects);
                listDataSets.Add(dataProperHandling);

                var inspectionTrainPieChartFullVm = new InspectionTrainPieChartFullVm();
                inspectionTrainPieChartFullVm.Labels = yearsList.Select(x=> x.Year.ToString()).ToArray();
                inspectionTrainPieChartFullVm.Datasets = listDataSets.ToArray();

                return inspectionTrainPieChartFullVm;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
