import { useState, useEffect } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import { Link } from 'react-router-dom';
import { getFeaturedPosts } from '../../Services/Widgets';

const FeaturedPostsWidget = () => {
    const [postList, setPostList] = useState([]);

    useEffect(() => {
        getFeaturedPosts().then(data => {
            if (data)
                setPostList(data);
            else
                setPostList([]);
        });
    }, [])

    return (
        <div className='mb-4'>
            <h3 className='text-success mb-2'>
                Top 3 bài viết được xem nhiều nhất
            </h3>
            {postList.length > 0 &&
                <ListGroup>
                    {postList.map((item, index) => {
                      return (
                        <ListGroup.Item key={index}>
                            <Link to={`/blog/post?${item.urlSlug}`}
                              title={item.description}
                              key={index}>
                              {item.title}
                              <span>&nbsp;({item.viewCount})</span>
                            </Link>
                        </ListGroup.Item>
                      );
                    })}
                </ListGroup>
            }
        </div>
    );
}
export default FeaturedPostsWidget;