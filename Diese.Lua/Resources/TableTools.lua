
-- TableTools : provide various tools on tables

TableTools = {}

function TableTools.Count(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end

function TableTools.ListToTable(list)
    local table = {}
    local it = list:GetEnumerator()
    while it:MoveNext() do
		table[#table+1] = it.Current
    end
    return table
end

function TableTools.DictionaryToTable(list)
    local table = {}
    local it = list:GetEnumerator()
    while it:MoveNext() do
		table[it.Current.Key] = it.Current.Value
    end
    return table
end