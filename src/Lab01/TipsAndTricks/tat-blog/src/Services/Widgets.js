import axios from 'axios';
import { get_api } from './Methods';

export function getCategories() {
    return get_api(`https://localhost:3000/api/categories`);
}

// export async function getCategories(pageSize = 10, pageNumber = 1 ) {
//     try {
//         const response = await
// axios.get(`https://localhost:7227/api/categories`);
//         const data = response.data;
//         if (data.isSuccess) {
//             return data.result;
//         }
//         else
//             return null;
//     } catch (error) {
//         console.log('Error', error.message);
//       return null;
//     }
// }

export async function getFeaturedPosts(pageSize = 10, pageNumber = 1 ) {
    try {
        const response = await
axios.get(`https://localhost:7227/api/posts/featured/3`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        }
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}

export async function getRandomPosts(pageSize = 10, pageNumber = 1 ) {
    try {
        const response = await
axios.get(`https://localhost:7227/api/posts/random/5`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        }
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}

export async function getTagCloudWidget() {
    try {
        const response = await
axios.get(`https://localhost:7227/api/tags/tagcloud`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        }
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}

export async function getBestAuthorWidget() {
    try {
        const response = await
axios.get(`https://localhost:7227/api/authors/best/4`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        }
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}

export async function getArchivesWidget() {
    try {
        const response = await
axios.get(`https://localhost:7227/api/posts/archives/12`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        }
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
      return null;
    }
}