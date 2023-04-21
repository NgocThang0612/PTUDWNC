import axios from "axios";

export function isEmptyOrSpaces(str) {
    return str === null || (typeof str === 'string' && str.match(/^ *$/) !== null);
}

export function getMonth(monthNumber){
    const month = [
        'January',
		'February',
		'March',
		'April',
		'May',
		'June',
		'July',
		'August',
		'September',
		'October',
		'November',
		'December',
    ];

    return month[monthNumber -1];
}

export function isInteger(str) {
	return Number.isInteger(Number(str)) && Number(str) >= 0;
}

export async function post_api(your_api, formData) {
	try {
		const response = await axios.post(your_api, formData);
		const data = response.data;
		if (data.isSuccess)
			return data.result;
		else
			return null;
	} catch (error) {
		console.log('Error', error.messsage);
		return null;
	}
}

export function decode(str) {
	let txt = new DOMParser().parseFromString(str, "text/html");
	return txt.documentElement.textContent;
}