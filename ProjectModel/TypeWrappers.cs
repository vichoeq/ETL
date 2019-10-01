using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel
{
    /// <summary>
    /// Representa una etapa de la construcción.
    /// </summary>
    public struct Phase : IEquatable<Phase>, IEquatable<string>
    {
        private string value;

        private Phase(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Crea una nueva etapa con un valor dado
        /// </summary>
        /// <param name="value">Nombre de la etapa</param>
        public static implicit operator Phase(string value)
        {
            return new Phase(value);
        }

        /// <summary>
        /// Obtiene en valor de esta etapa
        /// </summary>
        /// <param name="phase">La etapa a consultar</param>
        public static implicit operator string(Phase phase)
        {
            return phase.value;
        }

        /// <summary>
        /// Compara igualdad entre esta etapa y otra
        /// </summary>
        /// <param name="other">Referencia a la otra etapa</param>
        /// <returns>Si son iguales las dos etapas</returns>
        public bool Equals(Phase other)
        {
            return Equals(other.value);
        }

        /// <summary>
        /// Compara igualdad entre esta etapa y otra
        /// </summary>
        /// <param name="other">Nombre de la otra etapa</param>
        /// <returns>Si son iguales las dos etapas</returns>
        public bool Equals(string other)
        {
            // TODO ver igualdad independiente de las mayusculas tildes etc
            return value.Equals(other);
        }
    }

    /// <summary>
    /// Representa una zona de la construcción.
    /// </summary>
    public struct Zone : IEquatable<Zone>, IEquatable<int>
    {
        private int value;

        private Zone(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Crea una nueva zona con un valor dado
        /// </summary>
        /// <param name="value">ID de la zona</param>
        public static implicit operator Zone(int value)
        {
            return new Zone(value);
        }

        /// <summary>
        /// Obtiene en valor de esta zona
        /// </summary>
        /// <param name="zone">La zona a consultar</param>
        public static implicit operator int(Zone zone)
        {
            return zone.value;
        }

        /// <summary>
        /// Compara igualdad entre esta zona y otra
        /// </summary>
        /// <param name="other">Referencia a la otra zona</param>
        /// <returns>Si son iguales ambas zonas</returns>
        public bool Equals(Zone other)
        {
            return Equals(value, other.value);
        }

        /// <summary>
        /// Compara igualdad entre esta zona y otra
        /// </summary>
        /// <param name="other">ID de la otra zona</param>
        /// <returns>Si son iguales ambas zonas</returns>
        public bool Equals(int other)
        {
            return value.Equals(other);
        }
    }


    /// <summary>
    /// Representa un nivel de la construcción. Opera como un Int
    /// </summary>
    public struct Level : IEquatable<Level>, IEquatable<int>
    {
        private int value;

        private Level(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Crea un nuevo nivel con un valor dado
        /// </summary>
        /// <param name="value">El valor del nivel</param>
        public static implicit operator Level(int value)
        {
            return new Level(value);
        }

        /// <summary>
        /// Obtiene el valor del nivel dado
        /// </summary>
        /// <param name="level">El nivel a consultar</param>
        public static implicit operator int(Level level)
        {
            return level.value;
        }

        /// <summary>
        /// Compara igualdad entre dos niveles
        /// </summary>
        /// <param name="other">Referencia al otro nivel</param>
        /// <returns>Si ambos niveles son iguales</returns>
        public bool Equals(Level other)
        {
            return Equals(value, other.value);
        }

        /// <summary>
        /// Compara igualdad entre dos niveles
        /// </summary>
        /// <param name="other">Valor del otro nivel</param>
        /// <returns>Si ambos niveles son iguales</returns>
        public bool Equals(int other)
        {
            return value.Equals(other);
        }
    }
}
