﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diese.Lua.Properties {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Diese.Lua.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à 
        ///--[[ CoroutineManager : allow to manage a table of coroutine ]]--
        ///
        ///CoroutineManager = { Table = {} }
        ///
        ///-- Create a coroutine and add it to the table
        ///------ name : name of the coroutine
        ///------ f : function use as coroutine
        ///function CoroutineManager.Create(name, f)
        ///	if (CoroutineManager.Exists(name)) then
        ///		assert(CoroutineManager.Status(name) ~= &apos;dead&apos;,
        ///				&quot;Coroutine with name = \&quot;&quot; .. name .. &quot;\&quot; already exists.&quot;)
        ///	end
        ///	CoroutineManager.Table[name] = coroutine.create(f)
        ///end
        ///
        ///-- Resume a cor [le reste de la chaîne a été tronqué]&quot;;.
        /// </summary>
        public static string CoroutineManager {
            get {
                return ResourceManager.GetString("CoroutineManager", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à 
        ///-- TableTools : provide various tools on tables
        ///
        ///TableTools = {}
        ///
        ///function TableTools.Count(T)
        ///  local count = 0
        ///  for _ in pairs(T) do count = count + 1 end
        ///  return count
        ///end
        ///
        ///function TableTools.ListToTable(list)
        ///    local table = {}
        ///    local it = list:GetEnumerator()
        ///    while it:MoveNext() do
        ///		table[#table+1] = it.Current
        ///    end
        ///    return table
        ///end
        ///
        ///function TableTools.DictionaryToTable(list)
        ///    local table = {}
        ///    local it = list:GetEnumerator()
        ///    while it:MoveNext() do [le reste de la chaîne a été tronqué]&quot;;.
        /// </summary>
        public static string TableTools {
            get {
                return ResourceManager.GetString("TableTools", resourceCulture);
            }
        }
    }
}