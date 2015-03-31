
--[[ coroutine.manager : extension of "coroutine" to handle registered coroutines ]]--

coroutine.manager = { table = {}, currentlyRunning = nil }

-- Create a coroutine and add it to the table
------ name : name of the coroutine
------ f : function use as coroutine
function coroutine.manager.create(f, name)
	if (coroutine.manager.exists(name)) then
		assert(coroutine.manager.status(name) ~= 'dead',
				"Coroutine with name = \"" .. name .. "\" already exists.")
	end
	coroutine.manager.table[name] = { coroutine = coroutine.create(f), sleep = 0 }
end

-- Resume a corountine
------ name : name of the coroutine
------ elapsed : time elapsed since last resume
function coroutine.manager.resume(name, elapsed)
	assert(coroutine.manager.exists(name), "Coroutine with name = \"" .. name .. "\" doesn't exist.")
	assert(elapsed >= 0, "Elapsed time can't be negative !")
	coroutine.manager.table[name].sleep = coroutine.manager.table[name].sleep - elapsed
	if coroutine.manager.table[name].sleep > 0 then
		return { true }
	end
	coroutine.manager.currentlyRunning = name
	local results = { coroutine.resume(coroutine.manager.table[name].coroutine) }
	coroutine.manager.currentlyRunning = nil
	if coroutine.manager.table[name].sleep < 0 then
		coroutine.manager.table[name].sleep = 0
	end
	return results
end

-- Update all coroutines
------ elapsed : time elapsed since last update
function coroutine.manager.update(elapsed)
	assert(elapsed >= 0, "Elapsed time can't be negative !")
	local allResults = {}
    for name, value in pairs(coroutine.manager.table) do
		allResults[name] = coroutine.manager.resume(name, elapsed)
    end
	return allResults
end

-- Get the status of a coroutine
------ name : name of the coroutine
function coroutine.manager.status(name)
	assert(coroutine.manager.exists(name), "Coroutine with name = \"" .. name .. "\" doesn't exist.")
	local status = coroutine.status(coroutine.manager.table[name].coroutine)
	if (status == "suspended" and coroutine.manager.table[name].sleep > 0) then
		return "sleeping"
	end
	return status
end

-- Check if coroutine is in the table
------ name : name of the coroutine
function coroutine.manager.exists(name)
	return coroutine.manager.table[name] ~= nil
end

-- Clean dead coroutines
function coroutine.manager.clean()
    for name, value in pairs(coroutine.manager.table) do
		if coroutine.manager.status(name) == "dead" then
			coroutine.manager.table[name] = nil
		end
    end
end

-- Make a coroutine sleep during a specific time
------ name : name of the coroutine
------ time : duration of sleep in milliseconds
function coroutine.manager.sleep(time, ...)
	assert(time >= 0, "Duration of sleep can't be negative !")
	assert(coroutine.manager.currentlyRunning ~= nil and coroutine.manager.exists(coroutine.manager.currentlyRunning),
			"You can't call sleep outside of a coroutine handle by the manager !")
	coroutine.manager.table[coroutine.manager.currentlyRunning].sleep = time
	coroutine.yield(...)
end