import axios from 'axios';
import { get_api, post_api } from './Methods';

export async function getPosts( pageSize = 10, pageNumber = 1, sortColumn = '', sortOrder = '') {
    try {
        const response = await
axios.get(`https://localhost:7227/api/posts/get-post-filter?PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}
&SortOrder=${sortOrder}`);
        const data = response.data;
        if (data.isSuccess)
            return data.result;
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}

// export async function getPosts(keyword = '',
//     pageSize = 10,
//     pageNumber = 1,
//     sortColumn = '',
//     sortOrder = '') {
//     return get_api(`https://localhost:7227/api/posts?${keyword}&${pageSize}&${pageNumber}&${sortColumn}&${sortOrder}`);
// }

export async function getAuthors(name = '',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = '') {
    return get_api(`https://localhost:7227/api/authors?${name}&${pageSize}&${pageNumber}&${sortColumn}&${sortOrder}`);
}

export function getFilter() {
    return get_api('https://localhost:7227/api/posts/get-filter');
}

export function getPostsFilter(keyword = '', authorId = '', categoryId = '',
    year = '', month = '', pageSize = 10, pageNumber = 1, sortColumn = '',
    sortOrder = '') {
    let url = new URL('https://localhost:7227/api/posts/get-post-filter');
    keyword !== '' && url.searchParams.append('Keyword', keyword);
    authorId !== '' && url.searchParams.append('AuthorId', authorId);
    categoryId !== '' && url.searchParams.append('CategoryId', categoryId);
    year !== '' && url.searchParams.append('Year', year);
    month !== '' && url.searchParams.append('Month', month);
    sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
    sortOrder !== '' && url.searchParams.append('SortOrder', sortOrder);
    url.searchParams.append('PageSize', pageSize);
    url.searchParams.append('PageNumber', pageNumber);
    return get_api(url.href);
}

export async function getPostById(id = 0) {
    if (id > 0)
        return get_api(`https://localhost:7227/api/posts/${id}`);
    return null
}

export function addOrUpdatePost(formData) {
    return post_api('https://localhost:7227/api/posts', formData);
}
