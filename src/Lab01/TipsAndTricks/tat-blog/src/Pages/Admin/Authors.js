import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams } from 'react-router-dom';
import { getAuthor } from '../../Services/BlogReponsitory';
import Loading from '../../Components/Loading';

const Authors = () => {
    const [authorsList, setAuthorsList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true);

        useEffect(() => {
            document.title = 'Danh sách tác giả';
         
            getAuthor().then(data => {
                    if (data)
                        setAuthorsList(data);
                    else
                        setAuthorsList([]);
                    setIsVisibleLoading(false);
                });
        }, []);

    return (
        <>
            <h1>Danh sách tác giả </h1>
            {isVisibleLoading ? <Loading /> :
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>Tên tác giả</th>
                            <th>Email</th>
                            <th>Slug</th>
                            <th>Số bài viết</th>
                            <th>Ngày viết bài</th>
                        </tr>
                    </thead>
                    <tbody>
                        {authorsList.length > 0 ? authorsList.map((items, index) =>
                            <tr key={index}>
                                <td>
                                    <Link
                                        to={`/admin/authors/${items.id}`}
                                        className='text-bold'>
                                        {items.fullName}
                                    </Link>
                                </td>
                                <td>{items.email}</td>
                                <td>{items.urlSlug}</td>
                                <td>{items.postCount}</td>
                                <td>{items.joinedDate}</td> 
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className='text-danger text-center'>Không tìm thấy tác giả nào</h4>
                                </td>
                            </tr>}
                    </tbody>
                </Table>
            }
        </>
    );
}


export default Authors;