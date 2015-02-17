
--[[ CoroutineManager : allow to manage a table of coroutine ]]--

CoroutineManager = { Table = {} }

-- Create a coroutine and add it to the table
------ name : name of the coroutine
------ f : function use as coroutine
function CoroutineManager.Create(name, f)
	if (CoroutineManager.Exists(name)) then
		assert(CoroutineManager.Status(name) ~= 'dead',
				"Coroutine with name = \"" .. name .. "\" already exists.")
	end
	CoroutineManager.Table[name] = coroutine.create(f)
end

-- Resume a corountine
------ name : name of the coroutine
function CoroutineManager.Resume(name)
	assert(CoroutineManager.Exists(name), "Coroutine with name = \"" .. name .. "\" doesn't exist.")
	return coroutine.resume(CoroutineManager.Table[name])
end

-- Update all coroutines
function CoroutineManager.Update()
	local results = {}
    for name, cor in pairs(CoroutineManager.Table) do
		r = { coroutine.resume(cor) }
		results[name] = r
    end
	return results
end

-- Get the status of a coroutine
------ name : name of the coroutine
function CoroutineManager.Status(name)
	assert(CoroutineManager.Exists(name), "Coroutine with name = \"" .. name .. "\" doesn't exist.")
	return coroutine.status(CoroutineManager.Table[name])
end

-- Check if coroutine is in the table
------ name : name of the coroutine
function CoroutineManager.Exists(name)
	return CoroutineManager.Table[name] ~= nil
end