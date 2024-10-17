using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllWithDetails();
            return View(employees);
        }

        public IActionResult Details(int? id)
        {

            var employee = _employeeRepository.GetByIdWithDetails(id);
            return View(employee);
        }

        public IActionResult Create()
        {
            var viewModel = new EmployeeViewModel
            {
                Employee = new Employee(),
                Branches = _employeeRepository.GetBranches(),
                Roles = _employeeRepository.GetRoles(),
                Shifts = _employeeRepository.GetShifts()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel viewModel)
        {

            _employeeRepository.Create(viewModel.Employee);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {

            var employee = _employeeRepository.GetById(id);

            var viewModel = new EmployeeViewModel
            {
                Employee = employee,
                Branches = _employeeRepository.GetBranches(),
                Roles = _employeeRepository.GetRoles(),
                Shifts = _employeeRepository.GetShifts()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel viewModel)
        {

            _employeeRepository.Update(viewModel.Employee);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int? id)
        {

            var employee = _employeeRepository.GetByIdWithDetails(id);
            return View(employee);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Shifts()
        {
            return View(_employeeRepository.GetShifts());
        }

        public IActionResult ShiftEdit(int? id)
        {

            var shift = _employeeRepository.GetShiftsById(id);

            return View(shift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShiftEdit(int id, Shift shift)
        {

            _employeeRepository.UpdateShift(shift);

            return RedirectToAction(nameof(Shifts));

        }
    }
}
