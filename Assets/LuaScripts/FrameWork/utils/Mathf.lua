local Mathf				= class('Mathf')

Mathf.PI = 3.1415

--为了确保精度，故将a+t(b-1)进行拆分
function Mathf:Lerp( lower,upper,delta )
	return (1-delta) * lower + delta * upper
end

--线性插值一般式
function Mathf:ProtoLerp( pStart,p,pEnd,lower,upper)
	local t = (p - pStart) / (pEnd - pStart)
	if t >= 1 then
		return upper
	elseif t <= 0 then
		return lower
	end

	return  t * (upper - lower) + lower
end

function Mathf:Clip( min,max,val )
	if val < min then
		return 0
	end

	if val > max then
		return 0
	end

	return val
end

return Mathf