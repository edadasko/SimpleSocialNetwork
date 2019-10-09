using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SimpleSocialNetwork.Models;
using System.Text.Encodings.Web;

namespace SimpleSocialNetwork.TagHelpers
{
    public class LikeTagHelper : TagHelper
    {
        readonly IUsersRepository _repository;
        readonly User _user;
        readonly HashSet<int?> likesId;

        public LikeTagHelper(IUsersRepository repository)
        {
            _repository = repository;
            _user = ((List<User>)_repository.Users)[0];
            likesId = _repository.GetUsersLikes(_user).Select(l => l.PostId).ToHashSet();

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var postId = Convert.ToInt32(output.Attributes["postId"].Value.ToString());
            if (likesId.Contains(postId))
            {
                output.RemoveClass("not-liked", HtmlEncoder.Default);
                output.AddClass("liked", HtmlEncoder.Default);
            }
            output.TagName = "button";
        }
    }
}
