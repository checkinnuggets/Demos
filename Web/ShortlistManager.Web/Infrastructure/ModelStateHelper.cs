using System.Web.Mvc;

namespace ShortlistManager.Web.Infrastructure
{
    public static class ModelStateHelper
    {
        private static readonly string Key
            = typeof(ModelStateHelper).FullName;

        public static void PersistModelState(this ControllerBase controller)
        {
            controller.TempData[Key] = controller.ViewData.ModelState;
        }

        public static ModelStateDictionary RetrieveModelState(this ControllerBase controller)
        {
            return controller.TempData[Key] as ModelStateDictionary;
        }

        public static void ClearStoredModelState(this ControllerBase controller)
        {
            controller.TempData.Remove(Key);
        }


        public static string GetAttemptedValue(ModelStateDictionary modelState, string key)
        {
            var modelStateEntry = modelState[key];
            return modelStateEntry?.Value.AttemptedValue;
        }

    }
}