using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8_OS.Core.Models
{
    /// <summary>
    /// Класс, содержащий данные о сгенерированном объектном коде 
    /// для дальнейшего встравивания в итоговую выходную структуру данных
    /// </summary>
    internal class ObjectCodeGenerationResult
    {
        public string ObjectCode { get; set; }
        public int Offset { get; set; }
    }
}
