﻿using Cesxhin.AnimeManga.Domain.DTO;
using RepoDb.Attributes;

namespace Cesxhin.AnimeManga.Domain.Models
{
    [Map("episoderegister")]
    public class EpisodeRegister
    {
        [Primary]
        [Map("episodeid")]
        public string EpisodeId { get; set; }

        [Map("episodepath")]
        public string EpisodePath { get; set; }

        [Map("episodehash")]
        public string EpisodeHash { get; set; }

        //convert EpisodeRegister to EpisodeRegisterDTO
        public static EpisodeRegister EpisodeRegisterToEpisodeRegisterDTO(EpisodeRegisterDTO anime)
        {
            return new EpisodeRegister
            {
                EpisodeId = anime.EpisodeId,
                EpisodePath = anime.EpisodePath,
                EpisodeHash = anime.EpisodeHash
            };
        }
    }
}
